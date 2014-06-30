app.controller("TeamsController", function ($scope, filterFilter, $window, $modal, teamsService) {

    $scope.teams = function() {
        return teamsService.getAllTeams();
    }

    $scope.showId=function(id) {
        alert(id);
    }


    $scope.save = function () {
        var newTeam = {
            teamName: $scope.teamName,
            projectName: $scope.projectName,
            projectCode: $scope.projectCode,
            projectDescription: $scope.projectDescription
        };

        teamsService.saveTeam(newTeam).success(function (response) {
           
            if (angular.equals(JSON.stringify(true), response)) {
                //toaster.pop('success', "Successful", "You have successfully created a team");
                window.location.href = "/Teams/Index";

            }
        }).error();
    }


    $scope.team = {
        text: "",
        name: $scope.name
    };




    

});


// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

app.controller("ModalInstanceCtrl", function ($scope, $modalInstance,team) {

    //$scope.items = team;
    //$scope.names = "festo";

    $scope.ok = function () {
        console.log($scope.names);
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});