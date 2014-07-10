app.controller("TeamsController", function ($scope, filterFilter, $window, $modal, teamsService) {
    
    $scope.listOfSelectedUsers = [];


    teamsService.getAllTeams().success(function(teams) {
        $scope.teams = teams;
        console.log($scope.teams);
    }).error();

    teamsService.getAllUsers().success(function(users) {
        $scope.users = users;

    }).error();


    $scope.save = function () {

        var newTeam = {
            Name: $scope.teamName,
            UserIds: $scope.listOfSelectedUsers
        };


    teamsService.createNewTeam(newTeam).success(function (response) {

            //if (angular.equals(JSON.stringify(true), response)) {
            //    //toaster.pop('success', "Successful", "You have successfully created a team");
            //    window.location.href = "/Teams/Index";

            //}

            toastr.success(newTeam.Name + ' has been created successfully');
            $scope.teamForm.$setPristine();
            $scope.teamName = '';
            $scope.listOfSelectedUsers = [];

        }).error();
    }


    $scope.team = {
        text: "",
        name: $scope.name
    };

    /// open the authentication dialog
    $scope.delete = function (team) {
        var format = team.split(',');

       

        $scope.team = {
            Name: format[0],
            Id:format[1]
        };

        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: authenticationController,
            resolve: {
                item: function () {
                    return $scope.team;
                }
            }
        });

    };
});


var authenticationController = function ($scope, $modalInstance, item, teamsService) {

   // model to hold user password and comment
    $scope.teamToDelete = item;


    $scope.ok = function () {
        teamsService.deleteTeam($scope.teamToDelete).success(function (response) {
            
        }).error();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};




