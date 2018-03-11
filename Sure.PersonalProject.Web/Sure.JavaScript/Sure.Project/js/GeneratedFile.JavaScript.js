var editor;
var loadUrl = '';

//Ready toastr
$(function () {
    //提示框初始化
    toastr.options = { "closeButton": true, "debug": false, "progressBar": true, "positionClass": "toast-bottom-right", "onclick": null, "showDuration": "300", "hideDuration": "1000", "timeOut": "5000", "extendedTimeOut": "1000", "showEasing": "swing", "hideEasing": "linear", "showMethod": "fadeIn", "hideMethod": "fadeOut" };
    //勾选框
    $(".i-checks").iCheck({ checkboxClass: "icheckbox_square-green", radioClass: "iradio_square-green", });
    //代码编辑器
    editor = CodeMirror.fromTextArea(document.getElementById("MyCode"),
     { lineNumbers: true, matchBrackets: true, styleActiveLine: true, theme: "ambiance" });
    //加载下拉款
    $('.select-local').comboSelect();
    //隐藏主页图标
    $(".gohome").hide();
});

//列表表格加载
var dataTables = $(".dataTables-example").dataTable({
    "bPaginate": true,      //翻页功能
    "bLengthChange": false, //改变每页显示数据数量
    "bFilter": true,       //过滤功能
    "bSort": true,          //排序功能
    "bInfo": true,          //页脚信息
    "bAutoWidth": true,     //自动宽度
    "oLanguage": {
        "sLengthMenu": "每页显示 _MENU_ 条记录", "sZeroRecords": "抱歉,没有找到", "sInfo": "显示 _START_ 到 _END_ , 共 _TOTAL_ 条数据", "sInfoEmpty": "", "sSearch": "筛选:  ", "sInfoFiltered": "(从 _MAX_ 条数据中检索)", "oPaginate": { "sFirst": "首页", "sPrevious": "上一页", "sNext": "下一页", "sLast": "尾页" }, "sZeroRecords": "无数据"
    }, "aoColumns": [{ "data": 'SURE_GENERATED_ID', "sTitle": "序号", "sWidth": "50px" }
                , { "data": 'SURE_GENERATED_NAME', "sTitle": "名称" }
                , { "data": 'SURE_GENERATED_TYPE', "sTitle": "文件类型" }
                , { "data": 'SURE_GENERATED_PATH', "sTitle": "模板路径" }
                , { "data": 'SURE_GENERATED_ID', "sTitle": "操作", "bSortable": false }],
    "columnDefs": [
     {
         "targets": [5], "data": "SURE_GENERATED_ID",
         "render": function (data, type, full) {
             return '<button onclick="PreViewClick(this);" type="button" class="btn btn-primary btn-xs" value = "' + full.SURE_GENERATED_PATH + '" style="margin-left:20px;" >预 览</button>' +
                    '<a href="/Generating/DownGeneratingFile?path=' + full.SURE_GENERATED_PATH + '" class="btn btn-primary btn-xs" style="margin-left:20px;" value = "' + full.SURE_GENERATED_PATH + '">下 载</a>';
         }
     }
    ], "sAjaxSource": "/GeneratedFile/Get_SURE_FILEINFO"
});

//Angular Js 、 创建模型
angular.module('GeneratedFileApp', []).controller('GeneratedFileController', function ($scope, $http, $window) {

    //下拉框绑定数据库服务器
    $scope.localDB = [{ name: ".", value: "." }, { name: "(local)", value: "(local)" }, { name: "SURE", value: "SURE" }, { name: "10.112.13.193", value: "10.112.13.193" }];

    //登录到数据库，显示各数据库名
    $scope.Login = function (form) { if (form.$valid) { var localIP = $(".select-local").find("option:selected").text() == "" ? $(".combo-input").val() : $(".select-local").find("option:selected").text(); $http({ method: 'POST', url: '/GeneratedFile/LoginDB', data: { userAccount: $scope.userAccount, userPwd: $scope.userPwd, localIP: localIP } }).then(function successCallback(response) { if (response.data.message == 'Success') { $scope.resultDB = eval(response.data.content); toastr.success('登录成功 ！ '); } else { toastr.error(response.data.content); } }, function errorCallback(response) { toastr.error('请求错误,请联系管理员 ！ '); }); } };

    //联动数据库，查询出各库表名
    $scope.ChangeDBbyTable = function ($event) {
        $http({ method: 'POST', url: '/GeneratedFile/LoginTable', data: { localDB: $scope.localDataBase } }).then(function successCallback(response) { if (response.data.message == 'Success') { $scope.resultTable = eval(response.data.content); } else { toastr.error('该数据库没有表 ！ '); } }, function errorCallback(response) { toastr.error("请求错误,请联系管理员 ！ "); });
    }

    //读取模板
    $scope.ReadTemplateClick = function () {
        var marking; $("input[type='radio']:checked").each(function () { marking = this.value; });
        $http({ method: 'POST', url: '/GeneratedFile/ReadTemplate', data: { "marking": marking } }).then(function successCallback(response) { if (response.data.message == 'Success') { editor.setValue(response.data.content); } else { toastr.error('读取模板错误 ！ '); } }, function errorCallback(response) { toastr.error("请求错误,请联系管理员 ！ "); });
    }

    //保存模板
    $scope.SaveTemplateClick = function () {
        $http({ method: 'POST', url: '/GeneratedFile/SaveTemplate', data: { "content": editor.getValue() } }).then(function successCallback(response) { if (response.data.message == 'Success') { toastr.success('保存模板成功 ！ '); editor.setValue(""); } else { toastr.error('保存模板错误 ！ '); } }, function errorCallback(response) { toastr.error("请求错误,请联系管理员 ！ "); });
    }

    //生成文件
    $scope.GenerateClick = function (form) {
        var marking; $("input[type='radio']:checked").each(function () { marking = this.value; }); var Table = $scope.localDataTable;
        $http({ method: 'POST', url: '/GeneratedFile/GeneratedTemplate', data: { marking: marking, loacalDataBase: $scope.localDataBase, loacaDataTable: Table[0], strNamespace: $scope.Namespace, notesContent: $scope.notesContent } }).then(function successCallback(response) { if (response.data.message == 'Success') { toastr.success('生成成功 ！ '); dataTables.api().ajax.url("/GeneratedFile/Get_SURE_FILEINFO").load(); editor.setValue(response.data.content); } else { toastr.error('生成错误 ！ '); } }, function errorCallback(response) { toastr.error("请求错误,请联系管理员 ！ "); });
    }
});

//类文件预览
function PreViewClick(obj) { $.ajax({ method: 'POST', url: '/GeneratedFile/ReadGeneratedFile', data: { path: $(obj).attr('value') }, success: function (response) { if (response.message == 'Success') { editor.setValue(response.content); } }, error: function (response) { toastr.error("请求错误,请联系管理员 ！ "); } }); }