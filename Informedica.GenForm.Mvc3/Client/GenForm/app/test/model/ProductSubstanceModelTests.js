Ext.define('GenForm.test.model.ProductSubstanceModelTests', {

    describe: 'GenForm.model.product.ProductSubstance',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.ProductSubstance');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a SortOrder field', function () {
            expect(model.data.SortOrder).toBeDefined();
        });

        it('have a Unit field', function () {
            expect(model.data.Unit).toBeDefined();
        });

        it('have a Unit field', function () {
            expect(model.data.Unit).toBeDefined();
        });

        it('have a Unit field', function () {
            expect(model.data.Unit).toBeDefined();
        });
    }
});