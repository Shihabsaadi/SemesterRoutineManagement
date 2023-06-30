app.controller('timeSpanCtrl', function ($scope, timeSpanService) {
    var getTimeSpanList = function ()
    {
        timeSpanService.getTimeSpan().then(function (response) {
            $scope.TimeSpanList = response.data;
        })
    }
    getTimeSpanList()
   
    $scope.onClick = function (expression, obj) {
        var data = []
        switch (expression) {
            case "Sync":
                sync();
                break;
            case "EditTimeSpan":
                $scope.StartTime = formateTime(obj.StartTimeFormated)
                $scope.EndTime = formateTime(obj.EndTimeFormated)
                $scope.Id = obj.Id
                $scope.Status = obj.Status
                $scope.Sort = obj.Sort
                break;
            case "SaveTimeSpan":
                data=
                    {
                        Id: $scope.Id,
                        StartTime: $scope.StartTime,
                        EndTime: $scope.EndTime,
                        Status: $scope.Status,
                        Sort : $scope.Sort
                    }
                timeSpanService.saveTimeSpan(data).then(function (response) {

                    if (response.data) {
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: response.data.Message,
                            showConfirmButton: false,
                            timer: 1500
                        })
                        sync();
                    }

                });
                break;
            case "DeleteTimeSpan":
                swal.fire({
                    title: 'Are you sure?',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes'
                }).then(function (willDelete) {
                    console.log('willdelete', willDelete)
                    if (willDelete.value==true) {
                        data = { Id: obj.Id}
                        timeSpanService.deleteTimeSpan(data).then(function (response) {
                            if (response.data) {
                                swal.fire(
                                    'Deleted!',
                                    obj.StartTimeFormated + ' - ' + obj.EndTimeFormated + ' has been deactivated',
                                    'success'
                                )
                                sync();
                            }
                        });
                    }
                    else {
                        // Cancelled the delete action
                        console.log("Delete cancelled");
                    }
                });
                break;
             default:
        }
    }
    var sync = function () {
            $scope.StartTime = null
            $scope.EndTime = null
            $scope.Id = null
            $scope.Status = null
            getTimeSpanList()
    }
    var formateTime = function (time) {
        var timeParts = time.split(":");
        var hours = parseInt(timeParts[0], 10);
        var minutes = parseInt(timeParts[1].split(" ")[0], 10);
        var meridiem = timeParts[1].split(" ")[1];

        // Adjust hours if necessary for 12-hour format
        if (meridiem === "PM" && hours < 12) {
            hours += 12;
        }
        if (meridiem === "AM" && hours === 12) {
            hours = 0;
        }

        // Create a new Date object and set the time values
        var date = new Date();
        date.setHours(hours);
        date.setMinutes(minutes);

        // Format the time as "HH:mm" (e.g., 09:30)
        var formattedTime = ("0" + hours).slice(-2) + ":" + ("0" + minutes).slice(-2);
        return formattedTime;
    }
});