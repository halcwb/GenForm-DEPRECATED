Ext.define('GenForm.test.model.PackageNameModelTests', {
    describe: 'PackageNameModelShould',

    tests: function () {
        var model;

        beforeEach(function () {
           if (!model) model = Ext.create('GenForm.model.product.PackageName');
        });

        it('be defined', function () {
            expect(model).toBeDefined();
        });

        it('have a PackageName field', function () {
            expect(model.data.PackageName).toBeDefined();
        });
    }
});