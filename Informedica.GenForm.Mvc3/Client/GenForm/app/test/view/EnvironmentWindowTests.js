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

        me.getLogPathField = function () {
            return envRegWindow.getLogPathField();
        };

        me.setLogPathField = function (logPath) {
            me.getLogPathField().value = logPath;
        };

        me.getExportPathField = function () {
            return envRegWindow.getExportPathField();
        };

        me.setExportPathField = function (exportPath) {
            me.getExportPathField().value = exportPath;
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

        it('Have a field for the log path', function () {
            expect(envRegWindow.getLogPath).toBeDefined();
        });

        it('Have a field for the export path', function () {
            expect(envRegWindow.getExportPath).toBeDefined();
        });

        it('Be able to set the environment name field', function () {
            me.setEnvironmentNameField('test');
            expect(me.getEnvironmentNameField().value).toBe('test');
        });

        it('Be able to set the connection string field', function () {
            me.setConnectionStringField('test');
            expect(me.getConnectionStringField().value).toBe('test');
        });

        it('Be able to set log path string field', function () {
            me.setLogPathField('test');
            expect(me.getLogPathField().value).toBe('test');
        });

        it('Be able to set the export path field', function () {
            me.setExportPathField('test');
            expect(me.getExportPathField().value).toBe('test');
        });

        it('Have a register environment button', function () {
            expect(envRegWindow.getRegisterEnvironmentButton().isXType('button')).toBeTruthy();
        });
    }

});