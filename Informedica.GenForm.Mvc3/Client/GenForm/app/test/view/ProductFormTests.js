Ext.define('GenForm.test.view.ProductFormTests', {
    describe: 'ProductFormShould',

    tests: function () {
        var form;

        beforeEach(function () {
            if (!form) form = Ext.create('GenForm.view.product.ProductForm');
            return form;
        });

        it('can be created', function () {
            expect(form).toBeDefined();
        });

        it('have a fields property', function () {
            expect(form.fields).toBeDefined();
        });

        it('have a text field for productname', function () {
            expect(form.fields.ProductName).toBeDefined();
        });

        it('have a text field for productcode', function () {
            expect(form.fields.ProductCode).toBeDefined();
        });

        it('have a number field for quantity', function () {
            expect(form.fields.Quantity).toBeDefined();
        });

        it('have a combobox for generic', function () {
            expect(form.fields.GenericName).toBeDefined();
        });

        it('have a combobox for brand', function () {
            expect(form.fields.BrandName).toBeDefined();
        });

        it('have a combobox for shape', function () {
            expect(form.fields.ShapeName).toBeDefined();
        });

        it('have a combobox for package', function () {
            expect(form.fields.PackageName).toBeDefined();
        });

        it('have a combobox for unit', function () {
            expect(form.fields.UnitName).toBeDefined();
        });
    }
});