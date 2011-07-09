Ext.define('GenForm.test.model.ProductSubstanceModelTests', {

    describe: 'GenForm.model.product.ProductSubstance',

    tests: function () {
        var getProductSubstanceModel;

        getProductSubstanceModel = function () {
            return Ext.ModelManager.getModel('GenForm.model.product.ProductSubstance');
        };

        it('ProductSubstanceModel should be defined', function () {
            expect(typeof(getProductSubstanceModel())).toEqual('function');
        });

    }
});