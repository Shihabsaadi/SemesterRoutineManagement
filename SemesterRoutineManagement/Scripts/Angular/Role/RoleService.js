app.service("roleService", ['$http', function ($http) {
   
    this.getRole = function()
    {
        return $http.get("/Role/GetRoleList");
    }
    //Save Permission
    this.savePermission = function (data) {
        return $http.post('/SetAccess/SaveAccess', JSON.stringify(data))
    }
    this.saveRole = function (data) {
        return $http.post('/Role/SaveRole', JSON.stringify(data))
    }
    this.search = function (data) {
        return $http.post('/SetAccess/GetMemberList', JSON.stringify(data))
    }
}]);