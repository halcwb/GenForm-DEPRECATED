/**
 * Created by .
 * User: hal
 * Date: 6-5-11
 * Time: 20:16
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.model.product.BrandNameModel', function () {
    var getBrandNameModel, loadBrandNameModel, brandNameModel;

    getBrandNameModel = function () {
        return Ext.ModelManager.getModel('GenForm.model.product.BrandNameModel');
    };

    loadBrandNameModel = function () {
        brandNameModel = null;
        getBrandNameModel().load('', {
            callback: function (result) {
                brandNameModel = result;
            }
        });
    };

    it('BrandNameModel should be defined', function () {
        expect(getBrandNameModel()).toBeDefined();
    });

    it('After calling load an Instance of BrandNameModel should be created', function () {
        loadBrandNameModel();

        waitsFor(function () {
            return brandNameModel;
        }, 'fetching BrandNameModel');
    });

    it('BrandModel data contains a BrandName', function () {
        expect(brandNameModel.data.BrandName === 'Paracetamol').toBe(true);
    });

});