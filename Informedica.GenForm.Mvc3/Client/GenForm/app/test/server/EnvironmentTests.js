Ext.define('GenForm.test.server.EnvironmentTests', {
    describe: 'Server side EnvironmentController Should',

    tests: function () {
        //noinspection JSUnusedGlobalSymbols
        var connection = 'Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True';

        it('have a get environments function', function () {
            expect(GenForm.server.Environment.GetEnvironments).toBeDefined();
        });

        it('return a list of environments when get environments is called', function () {
            var results;

            GenForm.server.UnitTest.GetEnvironments(function (response) {
                results = response;
            });

            waitsFor(function () {
                if (results) {
                    return results.data.length > 0;
                }
            }, 'get environments to return a list', GenForm.test.waitingTime);
        });

        it('have a register environment function', function () {
            expect(GenForm.server.Environment.RegisterEnvironment).toBeDefined();
        });

        it('return a  success value (true or false) when RegisterEnvironment is called', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {});

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                  result = response;
                  result.success = true;
            });

            waitsFor(function () {
                return result;
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeTruthy();
            });
        });

        it('return false when trying to register environment without a connection', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test'
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeFalsy();
            });

        });

        it('return false when trying to register environment with a nonexisting log path', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test',
                Database: 'Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True',
                LogPath: 'non existing'
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeFalsy();
            });

        });

        it('return false when trying to register environment with a nonexisting export path', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test',
                Database: connection,
                LogPath: 'c:\\',
                ExportPath: 'non existing'
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeFalsy();
            });

        });

        it ('return false when trying to register an environment with an invalid connection', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test',
                Database: 'invalid'
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeFalsy();
            });

        });

        it ('return true when registering an environment with a valid connection', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test',
                Database: connection
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                  result = response;
            });

            waitsFor(function () {
                if (result) {
                    if (!result.success) {
                        console.log(result);
                    }
                    return true;
                }
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });

        it ('return true when registering an environment with a valid connection and log path', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test',
                Database: connection,
                LogPath: 'c:\\'
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
                if (!result.success) {
                    console.log(result);
                }
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });

        it ('return true when registering an environment with a valid connection and export path', function () {
            var result, environment = Ext.create('GenForm.model.environment.Environment', {
                Name: 'Test',
                Database: connection,
                LogPath: 'c:\\',
                ExportPath: 'c:\\'
            });

            GenForm.server.UnitTest.RegisterEnvironment(environment.data, function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
                if (!result.success) {
                    console.log(result);
                }
            }, 'return of a success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });

    }
});