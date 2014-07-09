app.controller("TeamsController", function ($scope, filterFilter, $window, $modal, teamsService) {



    $scope.listOfSelectedUsers = [];

    $scope.teams = function() {
        return teamsService.getAllTeams();
    }

    teamsService.getAllUsers().success(function(users) {
        $scope.users = users;

        console.log($scope.users);
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


    //$scope.select2Options = {
    //    'multiple': true,
    //    'simple_tags': true,
    //    'tags': []  // Can be empty list.
    //};


    

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