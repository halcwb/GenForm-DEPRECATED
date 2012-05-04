Ext.define('GenForm.test.model.UnitModelTests', {
    describe: 'UnitModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.Unit');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a UnitName field', function () {
            expect(model.data.Name).toBeDefined();
        });

    }
});