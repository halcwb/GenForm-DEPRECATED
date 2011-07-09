Ext.define('GenForm.test.usecase.AdvancedLoginTest', {
    describe: 'Advanced login tests that:',

    tests: function () {
        var me = this, queryHelper = Ext.create('GenForm.test.util.QueryHelper');

        me.getAdvancedLogin = function () {
            return Ext.ComponentQuery.query('userlogin fieldset')[0];
        };

        me.toggleAdvancedLogin = function () {
            me.getAdvancedLogin().toggle();
        };

        me.getSelectDatabaseCombo = function () {
            return queryHelper.getFormComboBox('userlogin', 'database');
        };

        me.comboContains = function (combo, value) {
            return false;
        };

        it('The user can select an advanced login option', function () {
            me.toggleAdvancedLogin();
            expect(me.getAdvancedLogin().collapsed).toBeFalsy();
        });

        it('Advance login has a combobox to select a database', function () {
            expect(me.getSelectDatabaseCombo()).toBeDefined();
        });

        it('The user can select a database to login', function () {
            var database = 'TestDatabase Indurain',
                comboBox = me.getSelectDatabaseCombo();

            expect(me.comboContains(comboBox, database)).toBeTruthy();
        });
    }
});