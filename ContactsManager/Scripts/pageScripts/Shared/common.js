
function getPrimaryEntityList(service, $scope, url) {
    var result = service.getEntityList(url);
    result.then(function (res) {
        $scope.PrimaryEntityList = ({
            data: res.data 
        });
    },
        function (err) {
            handleException($scope, err);
        });
    $scope.selectedEntity = null;
}

function handleException($scope, err, scopeName) {
    var errorMessage;
    if (err.status == 400) {
        if (err.data.ErrorList && err.data.ErrorList.length > 0) {
            if (scopeName)
                $scope[scopeName] = err.data.ErrorList;
            else
                $scope.validationErrors = err.data.ErrorList;
        }
        else if (err.data)
            errorMessage = err.data.replace(/"/g, "");
    }
    else if (err.status == 401) {
        if (err.data && err.data.Message)
            errorMessage = err.data.Message.replace(/"/g, "");
    }
    else if (err.status == 404) {
        errorMessage = err.data.MessageDetail.replace(/"/g, "");
    }
    else if (err.data && err.data.ExceptionMessage) {
        errorMessage = err.data.ExceptionMessage.replace(/"/g, "");
    }
    else if (err.data && err.data.ErrorList && err.data.ErrorList.length > 0) {
        if (scopeName)
            $scope[scopeName] = err.data.ErrorList;
        else
            $scope.validationErrors = err.data.ErrorList;
    }
    else {
        errorMessage = (err.data === "" || err.data === null) ? "Unknown Error" : err.data.replace(/"/g, "");
    }
    error(errorMessage, 'Status: Requested Information failed');
}

function Delete(service, $scope, id, url) {
    if (validSelection(id)) {
        swal({
            title: "Are you sure want to delete?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            confirmButtonColor: "#DD6B55",
            closeOnConfirm: true
        },

       function () {
           var result = service.delete(id, url);
           result.then(function (res) {
               $scope.selectedEntity = null;
               updateList($scope.PrimaryEntityList.data, res.data.Id, $scope);
               $scope.show = false;
               success('Requested Information Deleted Successfully', 'Status');
           }, function (err) {
               handleException($scope, err);
           });
       });
    }
}


function SaveContactsManager(service, $scope, url, primaryListUrl) {
    var entity = $scope.model;
    $scope.validationErrors = null;
    var reloadUrl = (primaryListUrl == null || primaryListUrl === undefined) ? url : primaryListUrl;
    
        var saveResult = service.post(entity, url);
        saveResult.then(function (res) {
            getPrimaryEntityList(service, $scope, reloadUrl);
            $scope.show = false;
            $scope.errorOccurred = false;
            success('Requested Information Added Successfully', 'Status');
        }, function (err) {
            $scope.show = true;
            $scope.errorOccurred = true;
            handleException($scope, err);
        });
     
}

function UpdateContactsManager(service, $scope, url, primaryListUrl) {
    var entity = $scope.model;
    $scope.validationErrors = null;
    var reloadUrl = (primaryListUrl == null || primaryListUrl === undefined) ? url : primaryListUrl;
    var editResult = service.post(entity, url);
    editResult.then(function (res) {
        getPrimaryEntityList(service, $scope, reloadUrl);
        $scope.show = false;
        $scope.errorOccurred = false;
        success('Requested Information Updated Successfully', 'Status');
    }, function (err) {
        $scope.show = true;
        $scope.errorOccurred = true;
        handleException($scope, err);
    });
}
 
    function updateList(alldata, id, $scope) {
        for (var i = alldata.length - 1; i >= 0; i--) {
            if (alldata[i].Id === id) {
                alldata.splice(i, 1);
                break;
            }
        } 
    }

    function validSelection(id) {
        if (id == null || id === undefined) {
            info('Please select atleast one row.', 'Information');
            return false;
        }
        return true;
    } 

   
 