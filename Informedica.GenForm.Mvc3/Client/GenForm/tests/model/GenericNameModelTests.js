/**
 * Created by .
 * User: hal
 * Date: 28-4-11
 * Time: 14:56
 * To change this template use File | Settings | File Templates.
 */

describe("GenForm.model.product.GenericName", function () {
    var callbackObject, setUpCallBackObject, getGenericNameModelClass,  getGenericNameModel, loadGenericNameModel,
        waitingTime = 100,
        proxy = {
            type: 'direct',
            directFn: Product.GetGenericNames
        };

    getGenericNameModel = function () {
        var model = Ext.ModelManager.getModel('GenForm.model.product.GenericName');
        model.setProxy(proxy);
        return model;
    };

    getGenericNameModelClass = function () {
        return Ext.ClassManager.get('GenForm.model.product.GenericName');
    };

    loadGenericNameModel = function () {
        genericNameModel = null;
        getGenericNameModel().load('', {
            callback: function (result) {
                genericNameModel = result;
            }
        });
    };

    it('Should exist in ClassManager', function () {
       expect(getGenericNameModelClass().modelName).toEqual('GenForm.model.product.GenericName');
    });

    it('ModelManager should return constructor for GenericNameModel', function() {
        expect(typeof(getGenericNameModelClass())).toEqual('function');
    });

    it('Calling load on model constructor should call doRequest of proxy', function() {
        var model = getGenericNameModel();
        spyOn(model.getProxy(), 'doRequest');

        model.load();

        expect(model.getProxy().doRequest).toHaveBeenCalled();
    });

    it('Calling load on model constructor should invoke a callback function', function () {
        setUpCallBackObject();

        getGenericNameModel().load('', {
            scope: callbackObject,
            callback: callbackObject.setCalledBackToTrue
        });

        waitsFor(callbackObject.getCalledBack, 'Calling proxy of GenericNameModel', waitingTime);
    });

    setUpCallBackObject = function () {
        callbackObject = {};
        callbackObject.isCalledBack = false;
        
        callbackObject.setCalledBackToTrue = function () {
            callbackObject.isCalledBack = true;
        };

        callbackObject.getCalledBack = function () {
            return !callbackObject.isCalledBack;

        };
    };

    it('After load an instance of GenericNameModel should be created', function () {
        var model;

        getGenericNameModel().load('', {
            callback: function (result) {
                model = result;
            }
        });

        waitsFor(function () { 
            if (model) {
                if (model.data) {
                    return model.data.GenericName === '' ? true: false;
                }
            }

            return false;
        }, 'Fetching GenericNameModel', waitingTime);
    });
    
});