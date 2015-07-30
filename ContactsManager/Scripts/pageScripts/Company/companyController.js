app.controller('clientController', function ($scope, service, quickLinkService, $compile, $localStorage, keyboardManager) {
    var url = config.appServicesHostName + "/api/Client";
    var taxUrl = config.appServicesHostName + "/api/Product";
    $scope.kendoGridColumns = [
     { field: "Name", title: "Client Name", template: "<span ng-if=!isEditEnabled>#=Name#</span><span ng-if=isEditEnabled><a href='\\#' class='link' ng-click='changeShow()'>#=Name#</a></span>" },
     { field: "RegistrationNumber", title: "Registration Number" },
     { field: "Email", title: "Email" },
     { field: "PhoneNumber", title: "Phone Number" }
    ];

    var quickLinkUrl = config.appServicesHostName + "/api/QuickLink";
    $scope.currentPageId = $localStorage.currentUser.currentPageId;
    $scope.currentUserId = $localStorage.currentUser.userId;
    $scope.quickLinkId = $localStorage.currentUser.currentQuickLinkId;
    $scope.isHaveQuickLink = false;
    angular.forEach($localStorage.currentUser.quickLinkMenu, function (item, key) {
        if (item.Id == $scope.currentPageId) {
            $scope.quickLinkId = item.QuickLink[0].Id;
            $scope.isHaveQuickLink = true;
            return;
        }
    });
    $("#divbreadcrumb").empty();
    if ($scope.isHaveQuickLink == true) {
        var breadCrumbs = '<ul class="page-breadcrumb breadcrumb breadcrumbheight"><li><a href="#" Id="Master">Master</a><i class="fa fa-arrow-right"></i></li> <li class="active"><a href="/Client/Client"><p class="Title">Client</p></a></li><li> <i class="' + config.messageText.deleteClass + '" title="' + config.messageText.deleteToolTip + '" ng-click="quickLinkDelete()"></i></li></ul>';
    }
    else {
        var breadCrumbs = '<ul class="page-breadcrumb breadcrumb breadcrumbheight"><li><a href="#" Id="Master">Master</a><i class="fa fa-arrow-right"></i></li> <li class="active"><a href="/Client/Client"><p class="Title">Client</p></a></li><li> <i class="' + config.messageText.saveClass + '" title="' + config.messageText.saveToolTip + '" ng-click="quickLinkSave()"></i></li></ul>';
    }

    $("#divbreadcrumb").html($compile(breadCrumbs)($scope));

    $scope.searchText = "";
    $scope.IsNewRecord = 1;

    $scope.isTaxTypeRbtnSelected = function (index) {
        return index === $scope.model.TaxType;
    };

    getPrimaryEntityList(service, $scope, url);

    bindTax(service, $scope, taxUrl + "/GetTax/0");
    bindVat(service, $scope, taxUrl + "/GetVat/0");
  
    $scope.rbtTax = function () {
        $scope.model.TaxId = "";
    }

    $scope.rbtVat = function () {
        $scope.model.TaxId = "";
    }
   

    $scope.chkClick = function () {
        if($scope.model.PhysicalAddress !== '')
        {
            $scope.model.PostalAddress= $scope.model.PhysicalAddress;
        }
    }

    $scope.quickLinkSave = function () {
        swal({
            title: config.messageText.addQuickLinkMsg,
            type: config.messageText.quickLinkMsgType,
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, Continue!",
            confirmButtonColor: "#DD6B55",
            closeOnConfirm: true
        },
         function () {

             quickLinkSave(quickLinkService, $scope, quickLinkUrl, $localStorage, service);
         });
    }

    $scope.quickLinkDelete = function () {
        swal({
            title: config.messageText.removeQuickLinkMsg,
            type: config.messageText.quickLinkMsgType,
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, Continue!",
            confirmButtonColor: "#DD6B55",
            closeOnConfirm: true
        },
          function () {
              quickLinkDelete(quickLinkService, $scope, $scope.quickLinkId, quickLinkUrl, service, $localStorage);
          });
    }
    $scope.save = function () {
        Save(service, $scope, url);
    };
    $scope.refresh = function () {
        $scope.searchText = "";
        getPrimaryEntityList(service, $scope, url);
    };
    $scope.search = function () {
        if ($scope.searchText == "")
            getPrimaryEntityList(service, $scope, url);
        else
            getPrimaryEntityListByFilter(service, $scope, url + "/GetClientByFilter/", $scope.searchText);
    };

    $scope.delete = function (id) {
        Delete(service, $scope, id, url);
    };
   
    $scope.edit = function (emp) {
        $scope.revertDirty();
        validSelection(emp);
        $scope.model = emp;
        $scope.disabled = false;
        $scope.show = true;
        $scope.IsNewRecord = 0;
        $scope.model.TaxType = entity.TaxType.toString();
        $scope.validationErrors = {};
        $('#btnSave').show();
    };

    $scope.view = function (emp) {
        validSelection(emp);
        $scope.model = emp;
        $scope.disabled = true;
        $scope.show = true;
        $scope.validationErrors = {};
        $('#btnSave').hide();
    };

    $scope.add = function () {
        $scope.revertDirty();
        $scope.IsNewRecord = 1;
        $scope.disabled = false;
        $scope.show = true;
        $scope.model = {};
        $scope.model.TaxType = "1";
        $scope.validationErrors = {};
        $('#btnSave').show();
    };

    $scope.cancel = function () {
        if ($scope.clientForm.$dirty) {
            swal({
                title: "There are some unsaved changes. Do you want continue without save?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes, Continue!",
                confirmButtonColor: "#DD6B55",
                closeOnConfirm: true
            },
           function () {
               $scope.back();
           });
        }
        else
            $scope.back();
    };
    $scope.back = function () {
        if ($scope.searchText == "")
            getPrimaryEntityList(service, $scope, url);
        else
            getPrimaryEntityListByFilter(service, $scope, url + "/GetClientByFilter/", $scope.searchText);
        $scope.show = false;
        $scope.validationErrors = {};
        $scope.model = {};
    }

    $scope.revertDirty = function () {
        $scope.clientForm.$setPristine();
    }

    $scope.enter = function (keyEvent) {
        if (keyEvent.which === 13)
            $scope.search();
    }

    $scope.actionButtonClick = function (parameter) {
        actionButtonClick($scope, parameter)
    }

    getlayoutcontrols($scope, $localStorage);

    $scope.changeShow = function () {
        if ($scope.isEditEnabled)
            $scope.edit($scope.selectedEntity);
    };

    keyboardManager.bind('ctrl+s', function () {
        if ($scope.show && $scope.isSaveEnabled)
            $scope.save();
    });

    keyboardManager.bind('esc', function () {
        if ($scope.show)
            $scope.cancel();
    });

    //$scope.uploadFile = function (input) {
    //    if (input.files && input.files[0]) {
    //        var reader = new FileReader();
    //        reader.onload = function (e) {

    //            //Sets the Old Image to new New Image
    //            $('#photo-id').attr('src', e.target.result);

    //            //Create a canvas and draw image on Client Side to get the byte[] equivalent
    //            var canvas = document.createElement("canvas");
    //            var imageElement = document.createElement("img");

    //            imageElement.setAttribute('src', e.target.result);
    //            canvas.width = imageElement.width;
    //            canvas.height = imageElement.height;
    //            var context = canvas.getContext("2d");
    //            context.drawImage(imageElement, 0, 0);
    //            var base64Image = canvas.toDataURL("image/jpeg");

    //            //Removes the Data Type Prefix 
    //            //And set the view model to the new value
    //            $scope.model.UserImage = base64Image.replace(/data:image\/jpeg;base64,/g, '');
    //        }

    //        //Renders Image on Page
    //        reader.readAsDataURL(input.files[0]);
    //    }
    //};
})
