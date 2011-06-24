/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/19/11
 * Time: 10:08 AM
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.lib.view.window.SaveCancelWindow', function () {
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
});