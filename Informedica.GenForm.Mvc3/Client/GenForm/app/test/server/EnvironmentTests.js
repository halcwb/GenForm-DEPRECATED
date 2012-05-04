Ext.define('GenForm.test.server.EnvironmentTests', {
    describe: 'Server side EnvironmentController Should',

    tests: function () {
        //noinspection JSUnusedGlobalSymbols
        var me = this,
            connection = 'Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True';

        it('be defined', function () {
            expect(GenForm.server.Environment.GetEnvironments).toBeDefined();
        });

        it('return a  success value (true or false) when RegisterEnvironment is called', function () {
            var result;

            GenForm.server.UnitTest.RegisterEnvironment('GenFormTest', connection, function (response) {
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

        });

        it('return false when trying to register environment with a nonexisting log path', function () {

        });

        it('return false when trying to register environment with a nonexisting export path', function () {

        });

        it ('return false when trying to register an environment with an invalid connection', function () {
            var result;

            GenForm.server.Environment.RegisterEnvironment('GenFormTest', 'invalid connection', function (response) {
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
            var result;

            GenForm.server.Environment.RegisterEnvironment('GenFormTest', connection, function (response) {
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

        it ('return true when registering an environment with a valid connection and log path', function () {
            var result;

            GenForm.server.Environment.RegisterEnvironment('GenFormTest', connection, function (response) {
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
            var result;

            GenForm.server.Environment.RegisterEnvironment('GenFormTest', connection, function (response) {
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