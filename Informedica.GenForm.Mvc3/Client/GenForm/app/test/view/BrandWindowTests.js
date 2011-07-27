Ext.define('GenForm.test.view.BrandWindowTests', {
    describe: 'BrandWindowShould',

    tests: function () {
        var window;

        beforeEach(function () {
            if (!window) window = Ext.create('GenForm.view.product.BrandWindow');
            return window;
        });

        it('be defined', function () {
           expect(window).toBeDefined();
        });

        it('have a brandform', function () {
           expect(window.forms.BrandForm).toBeDefined();
        });

        it('brandform should have a brandname', function () {
           expect(window.forms.BrandForm.fields.BrandName).toBeDefined();
        });

        it('should have a toolbar with a save and cancel button', function () {
            expect(window.toolbar.buttons.save).toBeDefined();
            expect(window.toolbar.buttons.cancel).toBeDefined();
        });
    }
});