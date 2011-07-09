Ext.define('GenForm.test.view.SaveCancelWindowTests', {

    describe: 'GenForm.lib.view.window.SaveCancelWindow',

    tests: function () {
        var getSaveCancelWindow, instance;

        getSaveCancelWindow = function (config) {
            if (!instance) {
                instance = Ext.create('GenForm.lib.view.window.SaveCancelWindow', config)
            }
            return instance;
        }

        it('can be created', function () {
            expect(getSaveCancelWindow()).toBeDefined();
        });

        it('should extend an Ext.window.Window', function () {
           expect(getSaveCancelWindow().superclass.$className).toBe('Ext.window.Window');
        });

        it('should have a savecancel toolbar', function () {
            expect(getSaveCancelWindow().getSaveCancelToolbar()).toBeDefined();
        });
    }
});