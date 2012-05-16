Ext.define('GenForm.test.usecase.AdvancedLoginTest', {
    describe: 'Advanced login tests that:',

    tests: function () {
        var me = this,
            queryHelper = Ext.create('GenForm.lib.util.QueryHelper'),
            messageChecker = Ext.create('GenForm.lib.util.MessageChecker'),
            windowName = 'environmentwindow',
            environment = 'GenFormTest',
            database = 'Data Source=HAL-WIN7\\INFORMEDICA;Initial Catalog=GenFormTest;Integrated Security=True',
            logPath = 'c:\\Development\\GenForm\\LogPath',
            exportPath = 'c:\\Development\\GenForm\\ExportPath',
            message = '';

        me.getLoginWindow = function () {
            return Ext.ComponentQuery.query('window[itemId=wndLogin]')[0]
        };

        me.getAdvancedLogin = function () {
            return me.getLoginWindow().getLoginForm().items.get('flsEnvironment');
        };

        me.toggleAdvancedLogin = function () {
            me.getAdvancedLogin().toggle();
        };

        me.getEnvironmentCombo = function () {
            return queryHelper.getFormComboBox('userlogin', 'Environment');
        };

        me.clickNewEnvironment = function () {
            queryHelper.clickButton(queryHelper.getButton('window', 'Nieuwe Omgeving'));
        };

        me.getEnvironmentWindow = function () {
            return queryHelper.getWindow(windowName);
        };

        me.getEnvironmentNameField = function () {
            return queryHelper.getFormTextField(windowName, 'Name');
        };

        me.getDatabaseField = function () {
            return queryHelper.getFormTextField(windowName, 'Database');
        };

        me.getLogPathField = function () {
            return queryHelper.getFormTextField(windowName, 'LogPath');
        };

        me.getExportPathField = function () {
            return queryHelper.getFormTextField(windowName, 'ExportPath');
        };

        me.clickRegisterEnvironmentButton = function () {
            queryHelper.clickButton(me.getRegisterEnvironmentButton());
        };

        me.getRegisterEnvironmentButton = function () {
            return queryHelper.getButton('window', 'Registreer Omgeving');
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
            expect(me.getEnvironmentCombo().isXType('combobox')).toBeTruthy();
        });

//        it('The select combobox has a list of environments', function () {
//            var isLoaded;

//            me.getEnvironmentCombo().store.load(function(records, operation, success) {
//                if (success) {
//                    isLoaded = true;
//                } else {
//                    console.log(operation);
//                    console.log(records);
//                }
//            });

//            waitsFor(function () {
//                return isLoaded;
//            }, 'environment combobox to load', GenForm.test.waitingTime);

//            runs(function () {
//                expect(me.getEnvironmentCombo().store.count() > 0).toBeTruthy()
//            });
//        });

        it('The user can open up a window to register a new environment', function () {
            me.clickNewEnvironment();
            expect(me.getEnvironmentWindow()).toBeDefined();
        });

        it('User can enter a environment name', function () {
            queryHelper.setFormField(me.getEnvironmentNameField(), environment);
            expect(me.getEnvironmentNameField().getValue()).toBe(environment);
        });

        it('User can enter a connection string', function () {
            queryHelper.setFormField(me.getDatabaseField(), database);
            expect(me.getDatabaseField().getValue()).toBe(database);
        });

        it('User can enter a log path', function () {
            queryHelper.setFormField(me.getLogPathField(), logPath);
            expect(me.getLogPathField().getValue()).toBe(logPath);
        });

        it('User can enter an export path', function () {
            queryHelper.setFormField(me.getExportPathField(), exportPath);
            expect(me.getExportPathField().getValue()).toBe(exportPath);
        });

        it('A environment can be registered', function () {
            message = environment;
            console.log(me.getEnvironmentWindow());
            me.clickRegisterEnvironmentButton();

            waitsFor(me.checkMessage, 'response of save', GenForm.test.waitingTime);
        });
    }
});