Ext.define('GenForm.test.model.UnitNameModelTests', {
    describe: 'UnitNameModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.UnitName');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a UnitName field', function () {
            expect(model.data.UnitName).toBeDefined();
        });
    }
});