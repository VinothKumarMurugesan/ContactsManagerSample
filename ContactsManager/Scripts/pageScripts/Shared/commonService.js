app.service('service', function ($http) {
     //Create new record
    this.post = function (entity, url) {
        var request = $http({
            method: "post",
            url: url,
            data: entity,
            
        });
        return request;
    };

    //Get Single Record
    this.get = function (id, url) {
         var request = $http({
            method: "get",
            url: url + "/" + id,
            async: true
        })
        return request;
    };

    //Get All Records
    this.getEntityList = function (url) {
        var request = $http({
            method: "get",             
            url: url,
            async: true
        })
        return request;
    };

 
   
   
    //Update single Record
    this.put = function (id, entity, url) {
      var request = $http({
            method: "put",
            url: url + "/"+ id,
            data: entity
        });
        return request;
    };

    
    //Delete single Record
    this.delete = function (id, url) {
        var request = $http({
            method: "delete",
            url: url + "/" + id
         
        });
       return request;
    };

    
});