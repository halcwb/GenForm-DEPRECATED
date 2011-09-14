Ext.define('GenForm.test.view.EnvironmentWindowTests', {
    describe: 'EnvironmentRegistrationWindowShould',

    tests: function () {
        var me = this, envRegWindow = Ext.create('GenForm.view.environment.EnvironmentWindow');

        me.getEnvironmentNameField = function () {
            return envRegWindow.getEnvironmentNameField();
        };

        me.setEnvironmentNameField = function (name) {
            me.getEnvironmentNameField().value = name;
        };
            
        me.getConnectionStringField = function () {
            return envRegWindow.getConnectionStringField();
        };

        me.setConnectionStringField = function (connection) {
            me.getConnectionStringField().value = connection;
        };

        it('Be defined', function () {
            expect(envRegWindow).toBeDefined();
        });

        it('Have a field for the environment name', function () {
            expect(envRegWindow.getEnvironmentName).toBeDefined();
        });

        it('Have a field for the connection string', function () {
            expect(envRegWindow.getConnectionString).toBeDefined();
        });

        it('Be able to set the environment name field', function () {
            me.setEnvironmentNameField('test');
            expect(me.getEnvironmentNameField().value).toBe('test');
        });

        it('Be able to set the connection string field', function () {
            me.setConnectionStringField('test');
            expect(me.getConnectionStringField().value).toBe('test');
        });
    }

});