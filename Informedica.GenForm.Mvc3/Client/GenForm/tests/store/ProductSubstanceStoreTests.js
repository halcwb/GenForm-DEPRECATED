/**
 * Created by .
 * User: hal
 * Date: 6-5-11
 * Time: 8:49
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.store.product.ProductSubstance', function () {
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

});