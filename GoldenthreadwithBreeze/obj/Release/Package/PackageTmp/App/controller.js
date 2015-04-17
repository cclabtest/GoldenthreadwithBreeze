/* TodoController - the controller for the "todo view" 
 * relies on Angular injector to provide:
 *     $q - promises manager
 *     $timeout - Angular equivalent of 'setTimeout'
 *     dataservice - the application data access service
 *     logger - the application's logging facility
 */
(function() {
  
    angular.module('app').controller('TodoController',
    ['$q', '$scope', '$timeout', 'dataservice', 'logger', controller]);

    function controller($q, $scope, $timeout, dataservice, logger) {
        // The controller's API to which the view binds
        
        var vm = this;
              
        vm.getTodos = getTodos;
        vm.items = [];
        // Start getting all the todos as soon as this controller is created
        getTodos();

        

        function getTodos() {
           
            // wait for Ng binding to set 'includeArchived' flag, then proceed
            getTodosImpl();

            function getTodosImpl() {
                dataservice.getTodos()
                    .then(querySucceeded);
            }

            function querySucceeded(data) {
                
               vm.items = data.results;
                //logger.info("Fetched Todos ");
            }
        };

       


    }
})();