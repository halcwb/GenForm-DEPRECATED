Ext.define('GenForm.test.view.BrandNameFormTests', {
    describe: 'BrandNameFormShould',

    tests: function () {
        var form;

        beforeEach(function () {
            if (!form) form = Ext.create('GenForm.view.product.BrandNameForm');
            return form;
        });

        it('be defined', function () {
            expect(form).toBeDefined();
        });

        it('have a fields property', function () {
           expect(form.fields).toBeDefined();
        });

        it('have a name field', function () {
            expect(form.fields.Name).toBeDefined();
        });
    }
});