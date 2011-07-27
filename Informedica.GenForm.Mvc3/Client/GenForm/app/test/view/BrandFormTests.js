Ext.define('GenForm.test.view.BrandFormTests', {
    describe: 'BrandFormShould',

    tests: function () {
        var form;

        beforeEach(function () {
            if (!form) form = Ext.create('GenForm.view.product.BrandForm');
            return form;
        });

        it('be defined', function () {
            expect(form).toBeDefined();
        });

        it('have a fields property', function () {
           expect(form.fields).toBeDefined();
        });

        it('have a brandname field', function () {
            expect(form.fields.BrandName).toBeDefined();
        });
    }
});