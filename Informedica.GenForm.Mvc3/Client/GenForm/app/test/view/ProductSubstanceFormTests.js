Ext.define('GenForm.test.view.ProductSubstanceFormTests', {
    describe: 'ProductSubstanceFormShould',

    tests: function () {
        var form;

        beforeEach(function () {
            if (!form) form = Ext.create('GenForm.view.product.ProductSubstanceForm');
            return form;
        });

        it('be defined', function () {
            expect(form).toBeDefined();
        });

        it('have a fields property', function () {
           expect(form.fields).toBeDefined();
        });

        it('have a substance combobox', function () {
            expect(form.fields.Substance).toBeDefined();
        });

        it('have a unit combobox', function () {
           expect(form.fields.Unit).toBeDefined();
        });

        it('have a order number field', function () {
            expect(form.fields.OrderNumber).toBeDefined();
        });

        it('have a quantity number field', function () {
            expect(form.fields.Quantity).toBeDefined();
        });

    }
});