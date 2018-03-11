var BASE_URL = 'js/plugins/webuploader';

var GUID = WebUploader.Base.guid();

var uploader = '';

//加载
$(function () {
    //隐藏图标
    $(".gohome").hide();
    //toastr 初始化
    toastr.options = { "closeButton": true, "debug": false, "progressBar": true, "positionClass": "toast-bottom-right", "onclick": null, "showDuration": "300", "hideDuration": "1000", "timeOut": "5000", "extendedTimeOut": "1000", "showEasing": "swing", "hideEasing": "linear", "showMethod": "fadeIn", "hideMethod": "fadeOut" };
    //webuploader - 并发上传（多线程上传）/ 初始化
    uploader = WebUploader.create({
        swf: '/Sure.JavaScript/Sure.WebUploader/Uploader.swf',  //兼容老版本IE
        server: '/FileUpload/SveFile',  // 文件接收服务端
        chunked: true,  // 开起分片上传
        chunkSize: 2 * 1024 * 1024,  //分片大小
        threads: 10,//上传并发数
        pick: {
            id: "#uploader-div #Select-File",
            label: '点击选择文件',
            multiple: true
        },   // 选择文件的按钮
        formData: { guid: GUID },
        chunkRetry: 5,
        fileNumLimit: 5,
        duplicate: false,
        threads: 5,
        disableGlobalDnd: true
    });

    // 当有文件被添加进队列的时候,添加到文件列表
    uploader.on('fileQueued', function (file) {
        var fileSezi = '';
        //判断大小分别是KB/MB/G
        if (eval(file.size / 1024).toFixed(2) < 1024) { fileSezi = eval(file.size / 1024).toFixed(2) + "KB"; } else if (eval(file.size / 1024 / 1024).toFixed(2) >= 1024) { fileSezi = eval(file.size / 1024 / 1024 / 1024).toFixed(2) + "G"; } else { fileSezi = eval(file.size / 1024 / 1024).toFixed(2) + "MB"; }
        var dataHtml = '<tr id="' + file.id + '">' +
                        '<td><strong>' + file.name + '</strong></td>' +
                        '<td>' + fileSezi + '</td>' +
                        '<td><div class="progress" style="margin-bottom: 0;">' +
                        '<div class="progress-bar" role="progressbar" style="width: 0%"></div>' +
                        '</div></td>' +
                        '<td class="text-center">' +
                        '<span id="isCancel"><i class="glyphicon glyphicon-ban-circle"></i></span>' +
                        '</td>' +
                        '<td>' +
                        '<button type="button" class="btn btn-success btn-xs" onclick="upload(this)">' +
                        '	<span class="glyphicon glyphicon-upload"></span> Upload' +
                        '</button>' +
                        '<button type="button" class="btn btn-warning btn-xs" onclick="cancel(this)">' +
                        '	<span class="glyphicon glyphicon-ban-circle"></span> Cancel' +
                        '</button>' +
                        '<button type="button" class="btn btn-danger btn-xs" onclick="remove(this)">' +
                        '	<span class="glyphicon glyphicon-trash"></span> Remove' +
                        '</button>' +
                        '</td>'
        //追加到dataTable
        $('#tbodyList').append(dataHtml);
    });

    //上传成功
    uploader.on('uploadSuccess', function (file) {
        //上传完成后，给后台发送一个合并文件的命令
        $.ajax({
            url: "/FileUpload/FileMerge",
            data: { "fileName": file.name, guid: GUID },
            type: "post",
            success: function (data) {
                //上传成功更改图标
                $('#' + file.id).children().eq(3).children().children().attr("class", "glyphicon glyphicon-ok");
                //上传成功,按钮解禁
                $("#but-Upload").removeAttr("disabled");
                //消息提示
                toastr.clear();
                toastr.success("上传成功");
            }
        });
    });

    // 点击触发上传
    $("#but-Upload").click(function () {
        if ($("#tbodyList tr").length > 0 && $("#tbodyList tr").length > uploader.getStats().successNum) {
            //上传按钮禁用
            $("#but-Upload").attr("disabled", "disabled");
            uploader.upload();
        }
        else { toastr.clear(); toastr.error("待上传列表为空", "消息提示"); }
    });

    // 文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id);
        //单个文件里的进度条
        $li.children().eq(2).children().children().css('width', percentage * 100 + '%');
        //底下的进度条
        $("#progressbarId").css('width', percentage * 100 + '%');
    });

    // 文件上传失败，显示上传出错
    uploader.on('uploadError', function (file) {
        //更改图标标签
        $('#' + file.id).children().eq(3).children().children().attr("class", "glyphicon glyphicon-remove");
        //toastr 错误提示
        toastr.clear(); toastr.error("上传失败,请联系管理员", "错误提示");
        //底下的进度条归0
        $(".progress-bar").css('width', 0 + '%');
        //上传成功,按钮解禁
        $("#but-Upload").removeAttr("disabled");
        //删除队列中的信息
        var fileArray = uploader.getFiles();
        for (var i = 0 ; i < fileArray.length; i++) {
            //取消文件上传
            uploader.cancelFile(fileArray[i]);
            //从队列中移除掉
            uploader.removeFile(fileArray[i], true);
        }
    });

    // 完成上传完成
    uploader.on('uploadComplete', function (file) { $('#' + file.id).find('.progress').fadeOut(); });
});

//全部上传删除
function clearQueue() {
    //进度条归零
    $(".progress-bar").css('width', 0 + '%');
    //列表删除
    $("#tbodyList").children().remove();
    //删除 Webuploader 中的队列
    var fileArray = uploader.getFiles();
    for (var i = 0 ; i < fileArray.length; i++) {
        //取消文件上传
        uploader.cancelFile(fileArray[i].id);
        //从队列中移除掉
        uploader.removeFile(fileArray[i].id, true);
    }
}

//单个删除
function remove(obj) {
    //文件Id
    var fileId = $(obj).parent().eq(0).parent().attr('id');
    //文件对象
    var dataFile = uploader.getFile(fileId);
    $(obj).parent().parent().remove();
    //取消文件上传
    uploader.cancelFile(fileId);
    //从 Webuploader 队列中删除
    uploader.removeFile(fileId, true);
}

//单个上传
function upload(obj) {
    var htmlId = $(obj).parent().eq(0).parent().attr('id');
    uploader.upload(htmlId);
}

//取消上传
function cancel(obj) {
    var htmlId = $(obj).parent().eq(0).parent().attr('id');
    var dataFile = uploader.getFile(htmlId);
    uploader.cancelFile(dataFile);
}

//取消全部上传
function cancelAll() {
    var dataFile = uploader.getFiles();
    for (var i = 0; i < dataFile.length; i++) {
        uploader.cancelFile(dataFile[i]);
    }
}