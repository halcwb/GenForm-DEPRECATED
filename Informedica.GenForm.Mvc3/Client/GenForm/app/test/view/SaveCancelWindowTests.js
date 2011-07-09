Ext.define('GenForm.test.view.SaveCancelWindowTests', {

    describe: 'GenForm.lib.view.window.SaveCancelWindow',

    tests: function () {
        var me = this, instance;

        me.getSaveCancelWindow = function (config) {
            if (!instance) {
                instance = Ext.create('GenForm.lib.view.window.SaveCancelWindow', config)
            }
            return instance;
        };

        me.hasSaveCancelToolbar = function (window) {
            return Ext.ComponentQuery.query('window[title=' + window.title + '] toolbar')[0];
        };

        it('can be created', function () {
            expect(me.getSaveCancelWindow()).toBeDefined();
        });

        it('should extend an Ext.window.Window', function () {
           expect(me.getSaveCancelWindow().superclass.$className).toBe('Ext.window.Window');
        });

        it('should have a savecancel toolbar', function () {
            expect(me.hasSaveCancelToolbar(me.getSaveCancelWindow())).toBeDefined();
        });
    }
});