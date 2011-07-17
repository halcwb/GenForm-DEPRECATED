Ext.define('GenForm.test.usecase.AdvancedLoginTest', {
    describe: 'Advanced login tests that:',

    tests: function () {
        var me = this,
            queryHelper = Ext.create('GenForm.lib.util.QueryHelper'),
            messageChecker = Ext.create('GenForm.lib.util.MessageChecker'),
            databaseName = 'TestDatabase',
            machine = 'HAL-WIN7',
            windowName = 'window[title=Registreer Database]',
            connection = 'Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True',
            message = '',
            waitingTime = 500;

        me.getAdvancedLogin = function () {
            return Ext.ComponentQuery.query('userlogin fieldset')[0];
        };

        me.toggleAdvancedLogin = function () {
            me.getAdvancedLogin().toggle();
        };

        me.getSelectDatabaseCombo = function () {
            return queryHelper.getFormComboBox('userlogin', 'database');
        };

        me.clickNewDatabase = function () {
            queryHelper.clickButton(queryHelper.getButton('window', 'Registreer Database'));
        };

        me.getRegisterDatabaseWindow = function () {
            return queryHelper.getWindow('window[title=Registreer Database]');
        };

        me.getDatabaseNameField = function () {
            return queryHelper.getFormTextField(windowName, 'databasename');
        };

        me.getMachineNameField = function () {
            return queryHelper.getFormTextField(windowName, 'machinename');
        };

        me.getConnectionField = function () {
            return queryHelper.getFormTextField(windowName, 'connectionstring');
        };

        me.clickRegisterDatabaseButton = function () {
            queryHelper.clickButton(me.getRegisterDatabaseButton());
        };

        me.getRegisterDatabaseButton = function () {
            return queryHelper.getButton('window', 'Opslaan');
        };

        me.checkMessage = function () {
            if (messageChecker.checkMessage(message)) {
                message = "";
                return true;
            } else {
                return false;
            }

        };

        it('The user can select an advanced login option', function () {
            me.toggleAdvancedLogin();
            expect(me.getAdvancedLogin().collapsed).toBeFalsy();
        });

        it('Advance login has a combobox to select a database', function () {
            expect(me.getSelectDatabaseCombo()).toBeDefined();
        });

        it('A default database is selected', function () {
            var database = 'Default Database',
                comboBox = me.getSelectDatabaseCombo();

            expect(comboBox.getValue()).toBe(database);
        });

        it('The user can select a database to login', function () {
            var database = 'TestDatabase Indurain',
                comboBox = me.getSelectDatabaseCombo();
            
            queryHelper.setFormField(comboBox, database);
            expect(comboBox.getValue()).toBe(database);
        });

        it('The user can open up a window to register a new database', function () {
            me.clickNewDatabase();
            expect(me.getRegisterDatabaseWindow()).toBeDefined();
        });

        it('User can enter a database name', function () {
            queryHelper.setFormField(me.getDatabaseNameField(), databaseName);
            expect(me.getDatabaseNameField().value).toBe(databaseName);
        });

        it('User can enter the machine name', function () {
            queryHelper.setFormField(me.getMachineNameField(), machine);
            expect(me.getMachineNameField().value).toBe(machine);
        });

        it('User can enter a connection string', function () {
            queryHelper.setFormField(me.getConnectionField(), connection);
            expect(me.getConnectionField().value).toBe(connection);
        });

        it('A database can be registered', function () {
            message = databaseName;
            me.clickRegisterDatabaseButton();

            waitsFor(me.checkMessage, 'response of save', waitingTime);
        });
    }
});