/**
 * Created by .
 * User: halcwb
 * Date: 5/15/11
 * Time: 10:00 AM
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.controller.product.ProductController', function () {
    var createProductController, testController;

    createProductController = function () {
        if (!testController) {
            testController = Ext.create('GenForm.controller.product.ProductController', {
                id: 'testProductController',
                application: GenForm.application
            });
        }
        return testController;
    };

    it('can be created', function () {
        console.log(createProductController());
        expect(createProductController()).toBeDefined();    
    });

    it('should have a getFormModelProductProductModel', function () {
        expect(createProductController().getGenFormModelProductProductModelModel).toBeDefined();
    });

    it('should have a saveProduct function', function () {
        expect(createProductController().saveProduct).toBeDefined();
    });
});