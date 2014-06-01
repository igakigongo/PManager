// defining our app namespace
window.myApp = {};

// defining our model (ProjectTask)
(function (myApp) {

    // Actual Constructor
    function Actual() {
        var self = this;
       
        self.Budget = ko.observable('0.00');

        self.StartDate = ko.observable('');

        self.EndDate = ko.observable('');
    }

    function Estimated() {
        var self = this;

        ko.Budget = ko.observable('0.00');

        ko.StartDate = ko.observable('');

        ko.EndDate = ko.observable('');
    }

    // ProjectTask Constructor Function
    function ProjectTask() {
        var self = this;

        self.Id = ko.observable('');

        self.ProjectId  = ko.observable('');

        self.UserId = ko.observable('');

        self.TaskName = ko.observable('');

        self.TaskDescription = ko.observable('');

        self.Actual = Actual;

        self.Estimated = Estimated;
    }
    myApp.Actual = Actual;
    myApp.Estimated = Estimated;
    myApp.ProjectTask = ProjectTask;
}(window.myApp))