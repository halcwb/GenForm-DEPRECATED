Ext.define('GenForm.test.view.DatabaseRegistrationWindowTests', {
    describe: 'DatabaseRegistrationWindowShould',

    tests: function () {
        var me = this, databaseregistrationWindow = Ext.create('GenForm.view.database.RegisterDatabaseWindow');

        me.getDatabaseNameField = function () {
            return databaseregistrationWindow.getDatabaseNameField();
        };

        me.setDatbaseNameField = function (name) {
            me.getDatabaseNameField().value = name;
        };
            
        me.getMachineNameField = function () {
            return databaseregistrationWindow.getMachineNameField();
        };

        me.setMachineNameField = function (name) {
            me.getMachineNameField().value = name;
        };

        me.getConnectionStringField = function () {
            return databaseregistrationWindow.getConnectionStringField();
        };

        me.setConnectionStringField = function (connection) {
            me.getConnectionStringField().value = connection;
        };

        it('Be defined', function () {
            expect(databaseregistrationWindow).toBeDefined();
        });

        it('Have a field for the database name', function () {
            expect(databaseregistrationWindow.getDatabaseName).toBeDefined();
        });
        
        it('Have a field for the machine name', function () {
            expect(databaseregistrationWindow.getMachineName).toBeDefined();
        });

        it('Have a field for the connection string', function () {
            expect(databaseregistrationWindow.getConnectionString).toBeDefined();
        });

        it('Be able to set the database name field', function () {
            me.setDatbaseNameField('test');
            expect(me.getDatabaseNameField().value).toBe('test');
        });

        it('Be able to set the machine name field', function () {
            me.setMachineNameField('test');
            expect(me.getMachineNameField().value).toBe('test');
        });

        it('Be able to set the connection string field', function () {
            me.setConnectionStringField('test');
            expect(me.getConnectionStringField().value).toBe('test');
        });
    }

});