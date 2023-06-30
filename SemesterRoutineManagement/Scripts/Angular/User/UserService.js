app.service("userService", ['$http', function ($http) {
   
    this.getUser = function()
    {
        return $http.get("/User/GetUserList");
    }
    this.getRole = function () {
        return $http.get("/Role/GetRoleList");
    }
    //Save 
    this.saveUser = function (data) {
        return $http.post('/User/SaveUser', JSON.stringify(data))
    }
    
}]);