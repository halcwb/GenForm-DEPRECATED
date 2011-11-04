Ext.define('GenForm.test.view.BrandNameWindowTests', {
    describe: 'BrandNameWindowShould',

    tests: function () {
        var window;

        beforeEach(function () {
            if (!window) window = Ext.create('GenForm.view.product.BrandNameWindow');
            return window;
        });

        it('be defined', function () {
           expect(window).toBeDefined();
        });

        it('have a brandnameform', function () {
           expect(window.forms.BrandNameForm).toBeDefined();
        });

        it('brandnameform should have a brandname', function () {
           expect(window.forms.BrandNameForm.fields.Name).toBeDefined();
        });

        it('should have a toolbar with a save and cancel button', function () {
            expect(window.toolbar.controls.buttons.save).toBeDefined();
            expect(window.toolbar.controls.buttons.cancel).toBeDefined();
        });
    }
});