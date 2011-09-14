Ext.define('GenForm.test.usecase.AdvancedLoginTest', {
    describe: 'Advanced login tests that:',

    tests: function () {
        var me = this,
            queryHelper = Ext.create('GenForm.lib.util.QueryHelper'),
            messageChecker = Ext.create('GenForm.lib.util.MessageChecker'),
            databaseName = 'TestDatabase',
            windowName = 'window[title=Registreer Database]',
            connection = 'Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True',
            message = '';

        me.getAdvancedLogin = function () {
            return Ext.ComponentQuery.query('userlogin fieldset')[0];
        };

        me.toggleAdvancedLogin = function () {
            me.getAdvancedLogin().toggle();
        };

        me.getSelectEnvironmentCombo = function () {
            return queryHelper.getFormComboBox('userlogin', 'environment');
        };

        me.clickNewEnvironment = function () {
            queryHelper.clickButton(queryHelper.getButton('window', 'Registreer Omgeving'));
        };

        me.getRegisterDatabaseWindow = function () {
            return queryHelper.getWindow('window[title=Registreer Database]');
        };

        me.getEnvironmentNameField = function () {
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

        it('Advance login has a combobox to select a environment', function () {
            expect(me.getSelectDatabaseCombo()).toBeDefined();
        });

        it('The user can open up a window to register a new environment', function () {
            me.clickNewDatabase();
            expect(me.getRegisterDatabaseWindow()).toBeDefined();
        });

        it('User can enter a environment name', function () {
            queryHelper.setFormField(me.getEnvironmentNameField(), databaseName);
            expect(me.getEnvironmentNameField().value).toBe(databaseName);
        });

        it('User can enter the machine name', function () {
            queryHelper.setFormField(me.getMachineNameField(), machine);
            expect(me.getMachineNameField().value).toBe(machine);
        });

        it('User can enter a connection string', function () {
            queryHelper.setFormField(me.getConnectionField(), connection);
            expect(me.getConnectionField().value).toBe(connection);
        });

        it('A environment can be registered', function () {
            message = databaseName;
            me.clickRegisterDatabaseButton();

            waitsFor(me.checkMessage, 'response of save', GenForm.test.waitingTime);
        });
    }
});