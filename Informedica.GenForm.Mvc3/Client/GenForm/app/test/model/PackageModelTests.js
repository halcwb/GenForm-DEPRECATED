Ext.define('GenForm.test.model.PackageModelTests', {
    describe: 'PackageModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.Package');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a PackageName field', function () {
            expect(model.data.Name).toBeDefined();
        });
    }
});