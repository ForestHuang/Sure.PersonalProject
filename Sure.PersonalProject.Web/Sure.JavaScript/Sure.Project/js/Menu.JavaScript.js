//Ready toastr
$(function () {
    //提示框初始化
    toastr.options = { "closeButton": true, "debug": false, "progressBar": true, "positionClass": "toast-bottom-right", "onclick": null, "showDuration": "300", "hideDuration": "1000", "timeOut": "5000", "extendedTimeOut": "1000", "showEasing": "swing", "hideEasing": "linear", "showMethod": "fadeIn", "hideMethod": "fadeOut" };
    //隐藏主页图标
    $(".gohome").hide();
});


//Menu 列表
var dataTablesMenu = $(".dataTables-example").dataTable({
    "bPaginate": true, "bLengthChange": false, "bFilter": true, "bSort": true, "bInfo": true, "bAutoWidth": true,
    "oLanguage": { "sLengthMenu": "每页显示 _MENU_ 条记录", "sZeroRecords": "抱歉,没有找到", "sInfo": "显示 _START_ 到 _END_ , 共 _TOTAL_ 条数据", "sInfoEmpty": "", "sSearch": "筛选:  ", "sInfoFiltered": "(从 _MAX_ 条数据中检索)", "oPaginate": { "sFirst": "首页", "sPrevious": "上一页", "sNext": "下一页", "sLast": "尾页" }, "sZeroRecords": "无数据" }
    , "aoColumns": [{ "data": 'SURE_MENU_ID', "sTitle": "序号", "sWidth": "50px" }
                , { "data": 'SURE_MENU_NAME_CN', "sTitle": "名称" }
                , { "data": 'SURE_MENU_URL', "sTitle": "URL" }
                , { "data": 'SURE_MENU_ICON', "sTitle": "ICON" }
                , { "data": 'SURE_USE_YN', "sTitle": "是否启用" }
                , { "data": 'SURE_CREATE_EMP', "sTitle": "创建人" }
                , { "data": 'SURE_MENU_DESC', "sTitle": "描述" }
                , { "data": 'SURE_MENU_ID', "sTitle": "操作", "bSortable": false }],
    "columnDefs": [
     {
         "targets": [7], "data": "SURE_MENU_ID",
         "render": function (data, type, full) {
             if (full.SURE_HIGH_MENU_ID != 0) {
                 return '<button onclick="ReadMenuData(this);" type="button" class="btn btn-primary btn-xs" value = "' + full.SURE_MENU_ID + '" style="margin-left:20px;" >编 辑</button>';
             } else {
                 return '<button onclick="ReadMenuData(this);" type="button" class="btn btn-primary btn-xs" value = "' + full.SURE_MENU_ID + '" style="margin-left:20px;" >编 辑</button>' +
                        '<button onclick="SaveMenuTwo(this);" type="button" class="btn btn-primary btn-xs" value = "' + full.SURE_MENU_ID + '" style="margin-left:20px;" >二 级</button>'
             }
         }
     }, { "targets": [4], "data": "SURE_USE_YN", "render": function (data, type, full) { if (full.SURE_USE_YN != true) { return '否'; } else { return '是'; } } }
    ], "sAjaxSource": "/Default/Get_Menu"
});

//Angular Js 、 创建模型
angular.module('MenuApp', []).controller('MenuController', function ($scope, $document, $http, $window, $interval) {

    $scope.SURE_MENU_NAME_CN_ONE_HIDE = false;  //一级菜单隐藏
    $scope.butSwitchEditShow = false; //切换按钮隐藏
    $scope.YNAdd = '';  //判断是否新增、1：新增，2：修改
    $scope.MenuId = ''; //MenuID
    $scope.SURE_MENU_NAME_CN_ONE = '0'; //父菜单ID
    $scope.SURE_MENU_NAME_CN_ONE_NAME = '父菜单';   //父菜单名称
    $scope.EXAMPLECLASS = 'onoffswitch-checkbox';  //开关按钮

    //实时执行
    $interval(function () { if ($("#txt_SURE_MENU_NAME_ICON").val() != undefined && $("#txt_SURE_MENU_NAME_ICON").val() != '') { $scope.SURE_MENU_NAME_ICON_CLASS = $("#txt_SURE_MENU_NAME_ICON").val(); } }, 500);

    //ICON
    $scope.SURE_MENU_NAME_ICON_Click = function (obj) {
        $("#txt_SURE_MENU_NAME_ICON").val(''); //清空
        $("#txt_SURE_MENU_NAME_ICON").emoji({
            showTab: true, animation: 'fade', button: '#txt_SURE_MENU_NAME_ICON',
            icons: [{
                name: "ICON", path: "/Sure.JavaScript/Images.icon/", maxNum: 50, file: ".jpg", placeholder: "{alias}",
                alias: {
                    1: "fa fa-adjust", 2: "fa fa-edit", 3: "fa fa-magic", 4: "fa fa-share", 5: "fa fa-asterisk"
                    , 6: "fa fa-envelope", 7: "fa fa-magnet", 8: "fa fa-share-alt", 9: "fa fa-ban-circle", 10: "fa fa-envelope-alt"
                    , 11: "fa fa-map-marker", 12: "fa fa-shopping-cart", 13: "fa fa-bar-chart", 14: "fa fa-exchange", 15: "fa fa-minus"
                    , 16: "fa fa-signal", 17: "fa fa-barcode", 18: "fa fa-exclamation-sign", 17: "fa fa-minus-sign", 18: "fa fa-signin", 19: "fa fa-beaker", 20: "fa fa-external-link"
                    , 21: "fa fa-mobile-phone", 22: "fa fa-signout", 23: "fa fa-beer", 24: "fa fa-eye-close", 25: "fa fa-money"
                    , 26: "fa fa-sitemap", 27: "fa fa-bell", 28: "fa fa-eye-open", 29: "fa fa-move", 30: "fa fa-sort"
                    , 31: "fa fa-bell-alt", 32: "fa fa-facetime-video", 32: "fa fa-music", 33: "fa fa-sort-down", 34: "fa fa-bolt", 35: "fa fa-fighter-jet"
                    , 36: "fa fa-off", 37: "fa fa-sort-up", 38: "fa fa-book", 39: "fa fa-film", 40: "fa fa-ok"
                    , 41: "fa fa-spinner", 42: "fa fa-bookmark", 43: "fa fa-filter", 44: "fa fa-ok-circle", 45: "fa fa-star"
                    , 46: "fa fa-bookmark-empty", 47: "fa fa-fire", 48: "fa fa-ok-sign", 49: "fa fa-star-empty", 50: "fa fa-briefcase"
                }
            }]
        });
    }

    //新增、修改,1：新增，2：修改
    $scope.SaveOrEdit = function (form) {
        $scope.butSwitchEditShow = false; //切换按钮隐藏
        //新增修改数据组装
        var requestData = {
            SURE_MENU_NAME_CN: $scope.SURE_MENU_NAME_CN,
            SURE_MENU_ICON: $("#txt_SURE_MENU_NAME_ICON").val() == undefined ? '' : $("#txt_SURE_MENU_NAME_ICON").val(),
            SURE_MENU_URL: $scope.SURE_MENU_URL == undefined ? '' : $scope.SURE_MENU_URL,
            SURE_MENU_DESC: $scope.SURE_MENU_DESC == undefined ? '' : $scope.SURE_MENU_DESC,
            SURE_MENU_REMARK: $scope.SURE_MENU_REMARK == undefined ? '' : $scope.SURE_MENU_REMARK,
            SURE_USE_YN: $scope.SURE_USE_YN == undefined || $scope.SURE_USE_YN == '' ? false : $scope.SURE_USE_YN,
            SURE_HIGH_MENU_ID: $scope.SURE_MENU_NAME_CN_ONE,  //一级菜单还是二级菜单,一级：0 二级：父级ID
            SURE_MENU_ID: $scope.MenuId == undefined ? '' : $scope.MenuId,  //ID用作修改
        }; var url = '';
        if (form.$valid) {
            //判断修改还是新增
            if ($scope.YNAdd == '2') url = '/Default/MenuEdit'; else url = '/Default/MenuSave';
            $http({
                method: 'POST', url: url, data: { requestData: angular.toJson(requestData) }
            }).then(function successCallback(response) {
                if (response.data.message == 'Success') {
                    toastr.success('操作成功 ！ ');
                    dataTablesMenu.api().ajax.url("/Default/Get_Menu").load();
                } else { toastr.error('操作失败 ！ '); }
            }, function errorCallback(response) { toastr.error('请求错误,请联系管理员 ！ '); });
        }
    }

    //修改读取数据
    $scope.ReadMenuData = function (menu_Id) {
        $scope.butSwitchEditShow = true;
        $http({
            method: 'POST', url: '/Default/Get_MenuByID', data: { SURE_MENU_ID: menu_Id }
        }).then(function successCallback(response) {
            //数据赋值
            var model = angular.fromJson(response);
            $scope.SURE_MENU_NAME_CN = model.data.SURE_MENU_NAME_CN;
            $("#txt_SURE_MENU_NAME_ICON").val(model.data.SURE_MENU_ICON);
            $scope.SURE_MENU_URL = model.data.SURE_MENU_URL;
            $scope.SURE_MENU_REMARK = model.data.SURE_MENU_REMARK;
            $scope.SURE_MENU_DESC = model.data.SURE_MENU_DESC;
            $scope.SURE_USE_YN = model.data.SURE_USE_YN;
            $scope.SURE_MENU_NAME_ICON_CLASS = model.data.SURE_MENU_ICON;

            $scope.YNAdd = '2'; //修改默认值
            $scope.MenuId = model.data.SURE_MENU_ID; //MenuID赋值
            //更改开关按钮Class
            if (model.data.SURE_USE_YN == true) { $scope.EXAMPLECLASS = 'onoffswitch-checkbox ng-untouched ng-valid ng-not-empty ng-dirty ng-valid-parse'; } else { $scope.EXAMPLECLASS = 'onoffswitch-checkbox ng-untouched ng-valid ng-dirty ng-valid-parse ng-empty'; }

            if (model.data.SURE_HIGH_MENU_ID == 0) { $scope.SURE_MENU_NAME_CN_ONE_HIDE = false; } else { $scope.SURE_MENU_NAME_CN_ONE_HIDE = true; $scope.SURE_MENU_NAME_CN_ONE = model.data.SURE_HIGH_MENU_ID; $scope.GetMenuNameByID(model.data.SURE_HIGH_MENU_ID); }

        }, function errorCallback(response) { toastr.error('请求错误,请联系管理员 ！ '); });
    }

    //切换为新增
    $scope.SwitchEdit = function () {
        $scope.YNAdd = '';
        $scope.SURE_MENU_NAME_CN = '';
        $("#txt_SURE_MENU_NAME_ICON").val('');
        $scope.SURE_MENU_URL = '';
        $scope.SURE_MENU_REMARK = '';
        $scope.SURE_MENU_DESC = '';
        $scope.SURE_USE_YN = '';
        $scope.SURE_MENU_NAME_CN_ONE = '0';
        $scope.SURE_MENU_NAME_CN_ONE_NAME = '父菜单';
        $scope.SURE_MENU_NAME_CN_ONE_HIDE = false;
        $scope.butSwitchEditShow = false;
        $scope.SURE_MENU_NAME_ICON_CLASS = '';

    }

    //新增二级菜单
    $scope.SaveMenuTwo = function (menu_Id) {
        $http({
            method: 'POST', url: '/Default/Get_MenuByID', data: { SURE_MENU_ID: menu_Id }
        }).then(function successCallback(response) {
            var model = angular.fromJson(response);
            $scope.SURE_MENU_NAME_CN_ONE_NAME = model.data.SURE_MENU_NAME_CN;
            if (model.data.SURE_HIGH_MENU_ID != 0) {
                $scope.SURE_MENU_NAME_CN_ONE_HIDE = false;
            } else {
                $scope.SURE_MENU_NAME_CN_ONE_HIDE = true; //父级菜单显示
                $scope.YNAdd = ''; //新增

                $scope.SURE_MENU_NAME_CN = '';
                $("#txt_SURE_MENU_NAME_ICON").val('');
                $scope.SURE_MENU_URL = '';
                $scope.SURE_MENU_REMARK = '';
                $scope.SURE_MENU_DESC = '';
                $scope.SURE_MENU_NAME_ICON_CLASS = '';
                //父级菜单赋值
                $scope.SURE_MENU_NAME_CN_ONE = model.data.SURE_MENU_ID;
                $scope.SURE_MENU_NAME_CN_ONE_NAME = model.data.SURE_MENU_NAME_CN;
                $scope.MenuId = undefined; //MenuID赋值

            }
        }, function errorCallback(response) { toastr.error('请求错误,请联系管理员 ！ '); });
    }

    //获取菜单名
    $scope.GetMenuNameByID = function (menu_Id) {
        $http({
            method: 'POST', url: '/Default/Get_MenuByID', data: { SURE_MENU_ID: menu_Id }
        }).then(function successCallback(response) {
            var model = angular.fromJson(response);
            $scope.SURE_MENU_NAME_CN_ONE_NAME = model.data.SURE_MENU_NAME_CN;
        }, function errorCallback(response) { toastr.error('请求错误,请联系管理员 ！ '); });
    }
});


//修改
function ReadMenuData(obj) { angular.element(document.querySelector("[ng-controller=MenuController]")).scope().ReadMenuData($(obj).attr('value')); }

//新增二级菜单
function SaveMenuTwo(obj) { angular.element(document.querySelector("[ng-controller=MenuController]")).scope().SaveMenuTwo($(obj).attr('value')); }