app.service("roomService", ['$http', function ($http) {
   
    this.getRoom = function()
    {
        return $http.get("/Room/GetRoomList");
    }
    this.saveRoom = function (data) {
        return $http.post('/Room/SaveRoom', JSON.stringify(data))
    }
  
}]);