app.service('teamsService', function($http) {
    this.saveTeam = function (params) {
        return $http.post('/Teams/Create?teamName=' + params.teamName + '&&projectName=' + params.projectName + '&&projectCode=' + params.projectCode + '&&projectDescription=' + params.projectDescription + '');
    }

    this.getAllTeams = function() {
        return $http.get('/Teams/GetTeams' );
    }

    this.getAllUsers = function() {
        return $http.get('/User/GetAllUsers');
    }

    this.createNewTeam = function (params) {
        console.log(params);
        return $http.post('/Teams/CreateTeam', {teamViewModel:params});
    }
});