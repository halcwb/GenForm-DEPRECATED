Ext.define('GenForm.test.view.EnvironmentWindowTests', {
    describe: 'EnvironmentRegistrationWindowShould',

    tests: function () {
        var me = this, envRegWindow;

        beforeEach(function () {
           if (!envRegWindow) {
               envRegWindow = Ext.create('GenForm.view.environment.EnvironmentWindow');
               envRegWindow.show();
           }
        });

        me.getEnvironmentNameField = function () {
            return envRegWindow.getEnvironmentNameField();
        };

        me.setEnvironmentNameField = function (name) {
            me.getEnvironmentNameField().setValue(name);
        };
            
        me.getDatabaseField = function () {
            return envRegWindow.getDatabaseField();
        };

        me.setDatabaseField = function (database) {
            me.getDatabaseField().setValue(database);
        };

        me.getLogPathField = function () {
            return envRegWindow.getLogPathField();
        };

        me.setLogPathField = function (logPath) {
            me.getLogPathField().setValue(logPath);
        };

        me.getExportPathField = function () {
            return envRegWindow.getExportPathField();
        };

        me.setExportPathField = function (exportPath) {
            me.getExportPathField().setValue(exportPath);
        };

        it('Be defined', function () {
            expect(envRegWindow).toBeDefined();
            envRegWindow.show();
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
            expect(me.getExportPathField()).toBeDefined();
        });

        it('Be able to set the environment name field', function () {
            me.setEnvironmentNameField('test');
            expect(me.getEnvironmentNameField().getValue()).toBe('test');
        });

        it('Be able to set the database string field', function () {
            me.setDatabaseField('test');
            expect(me.getDatabaseField().getValue()).toBe('test');
        });

        it('Be able to set log path string field', function () {
            me.setLogPathField('test');
            expect(me.getLogPathField().getValue()).toBe('test');
        });

        it('Be able to set the export path field', function () {
            me.setExportPathField('test');
            expect(me.getExportPathField().getValue()).toBe('test');
        });

        it('Have a register environment button', function () {
            expect(envRegWindow.getRegisterEnvironmentButton().isXType('button')).toBeTruthy();
        });

        it('have a update model function', function () {
            expect(envRegWindow.updateModel).toBeDefined();
        });

        it('set the model fields of the model', function () {
            var model = Ext.create('GenForm.model.environment.Environment', {});
            me.setEnvironmentNameField('Test');
            me.setDatabaseField('Test');
            me.setLogPathField('Test');
            me.setExportPathField('Test');

            model = envRegWindow.updateModel(model);

            console.log(model);
            expect(model.data.Name).toBe('Test');
            expect(model.data.Database).toBe('Test');
            expect(model.data.LogPath).toBe('Test');
            expect(model.data.ExportPath).toBe('Test');
        });
    }

});