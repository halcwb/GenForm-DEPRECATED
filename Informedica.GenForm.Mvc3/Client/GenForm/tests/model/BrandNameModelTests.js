/**
 * Created by .
 * User: hal
 * Date: 6-5-11
 * Time: 20:16
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.model.product.BrandName', function () {
    var getBrandNameModel,
        waitingTime = 100;

    getBrandNameModel = function () {
        var model = Ext.ModelManager.getModel('GenForm.model.product.BrandName');
        model.setProxy({
            type: 'direct',
            directFn: Product.GetBrandNames
        })

        return model;
    };

    it('BrandNameModel should be defined', function () {
        expect(getBrandNameModel()).toBeDefined();
    });

    it('After calling load an Instance of BrandNameModel should be created', function () {
        var model;

        getBrandNameModel().load('', {
            callback: function (record) {
                model = record;
            }
        })

        waitsFor(function () {
            return model ? true: false;
        }, 'fetching BrandNameModel', waitingTime);
    });

    it('BrandModel data contains a BrandName', function () {
        var model;

        getBrandNameModel().load('', {
            callback: function (result) {
                model = result;
            }
        });

        waitsFor(function () {
            if (model) {
                if (model.data) {
                    return model.data['BrandName'] !== undefined ? true: false;
                }
            }
            return false;
        }, 'checking if model contains BrandName prop', waitingTime);
    });

});