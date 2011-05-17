/**
 * Created by .
 * User: hal
 * Date: 5-5-11
 * Time: 14:15
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.model.product.ProductSubstanceModel', function () {
    var getProductSubstanceModel;

    getProductSubstanceModel = function () {
        return Ext.ModelManager.getModel('GenForm.model.product.ProductSubstanceModel');
    };

    it('ProductSubstanceModel should be defined', function () {
        expect(typeof(getProductSubstanceModel())).toEqual('function');
    });

});