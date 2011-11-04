Ext.define('GenForm.test.model.ShapeModelTests', {
    describe: 'ShapeModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.Shape');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a ShapeName field', function () {
            expect(model.data.Name).toBeDefined();
        });
    }
});