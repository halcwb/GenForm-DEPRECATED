Ext.define('GenForm.test.view.component.SaveCancelToolbarTests', {
    describe: 'SaveCancelToolbarShould',

    tests: function () {
        var toolbar;

        beforeEach(function () {
            if (!toolbar) toolbar = Ext.create('GenForm.lib.view.component.SaveCancelToolbar');
            return toolbar;
        });

        it('be defined', function () {
            expect(toolbar).toBeDefined();
        });

        it('has a buttons property', function () {
            expect(toolbar.buttons).toBeDefined();
        });

        it('has a save button', function () {
            expect(toolbar.buttons.save).toBeDefined();
        });

        it('has a cancel button', function () {
            expect(toolbar.buttons.cancel).toBeDefined();
        });
    }
});