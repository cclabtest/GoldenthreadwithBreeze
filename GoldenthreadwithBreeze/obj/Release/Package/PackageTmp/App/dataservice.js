/* dataservice: data access and model management layer 
 * relies on Angular injector to provide:
 *     $timeout - Angular equivalent of 'setTimeout'
 *     breeze - the Breeze.Angular service (which is breeze itself)
 *     logger - the application's logging facility
 */
(function() {

    angular.module('app').factory('dataservice',
    ['$http', '$q', '$timeout', 'breeze', 'logger', dataservice]);

    function dataservice($http, $q, $timeout, breeze, logger) {     

        var serviceName = 'http://localhost:51341/api/Chapters/'; // route to the same origin Web Api controller

        // *** Cross origin service example  ***
        // When data server and application server are in different origins
        //var serviceName = 'http://sampleservice.breezejs.com/api/todos/';
        

        var manager = new breeze.EntityManager(serviceName);
    

        var service = {
           
            getTodos: getTodos
           
        };
        return service;

        /*** implementation details ***/

       
        function getTodos() {
            var ajaxAdapter = breeze.config.getAdapterInstance('ajax');
            ajaxAdapter.defaultSettings = {
                headers: { "AuthToken": "73DE4898-C1D3-42A4-B802-0B81F1E3867F" },
            };
            var query = breeze.EntityQuery
                .from("Get")
                .top(5)
                .where("Title", "startsWith", "A")
                .orderBy("Title");
           
            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                alert(error.message);
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

       
        }

       
      
        //#endregion
    
})();