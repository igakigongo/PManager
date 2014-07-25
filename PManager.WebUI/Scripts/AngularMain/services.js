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

    this.deleteTeam = function (params) {
        return $http.post('/Teams/Delete', { teamId: params.Id });
    }

    this.getTeamToEdit = function(parameters) {
        return $http.post('/Teams/GetTeam', { teamId: parameters });
    }

    this.editTeam = function (params) {
        return $http.post('/Teams/EditTeam', { teamViewModel: params });
    }
});

app.service('resourceService', function($http) {
    this.AssignLaptopToProject = function(parameters) {
        return $http.post('/ProjectResource/AssignLaptop', { laptopId: parameters.resourceId, projectId: parameters.ProjectId });
    }

    this.AssignVehicleToProject = function (parameters) {
        return $http.post('/ProjectResource/AssignVehicle', { vehicleId: parameters.resourceId, projectId: parameters.ProjectId });
    }
});



app.service('projectService', function ($http) {
    this.getAllProjects = function () {
        return $http.post('/Project/GetProjects');
    }
});


app.service('taskService', function($http) {
    
    this.saveNewTask = function (params) {
        console.log(params);
        return $http.post('/ProjectTask/AddTask', { model: params });
    }

    this.getProjectTasks = function(id) {
        return $http.post('/ProjectTask/GetTasks?projectId='+id+'');
    };
});