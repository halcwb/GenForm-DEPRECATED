Ext.define('GenForm.test.store.ProductSubstanceStoreTests', {

    describe: 'GenForm.store.product.ProductSubstance',

    tests: function () {
        var createProductSubstanceStore, getProductSubstanceStore;

        createProductSubstanceStore = function () {
            return Ext.create('GenForm.store.product.ProductSubstance');
        };

        getProductSubstanceStore = function () {
            return Ext.getStore('productsubstancestore');
        };

        it('GenForm.store.product.ProductSubstanceStore should be created', function () {
            expect(createProductSubstanceStore()).toBeDefined();
        });

        it('GenForm.store.product.ProductSubstanceStore should be defined', function () {
            expect(getProductSubstanceStore()).toBeDefined();
        });

    }
});