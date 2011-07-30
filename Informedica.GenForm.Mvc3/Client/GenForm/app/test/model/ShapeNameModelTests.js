Ext.define('GenForm.test.model.ShapeNameModelTests', {
    describe: 'PackageNameModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.ShapeName');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a ShapeName field', function () {
            expect(model.data.ShapeName).toBeDefined();
        });
    }
});