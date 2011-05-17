/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 21:57
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.controller.product.ProductController', {
    extend: 'Ext.app.Controller',
    alias: 'widget.productcontroller',

    models: [
        'GenForm.model.product.ProductModel',
        'GenForm.model.product.GenericNameModel',
        'GenForm.model.product.BrandNameModel',
        'GenForm.model.product.ShapeNameModel',
        'GenForm.model.product.PackageNameModel',
        'GenForm.model.product.ProductRouteModel',
        'GenForm.model.product.ProductSubstanceModel'
    ],

    stores: [
        'GenForm.store.product.GenericNameStore',
        'GenForm.data.ProductSubstanceTestData'//'GenForm.store.product.ProductSubstanceStore'
    ],

    views: [
        'GenForm.view.product.ProductWindow',
        'GenForm.view.product.ProductForm',
        'GenForm.view.product.ProductSubstanceForm',
        'GenForm.view.product.ProductSubstanceGrid'
    ],
    
    init: function() {
        this.control({
            'productwindow > toolbar button[action=save]': {
                click: this.showSaveMessage
            },
            'productwindow > toolbar button[action=cancel]': {
                click: this.showCancelMessage
            }
        });
        
    },

    showSaveMessage: function () {
        Ext.MessageBox.alert('Save product');
    },

    showCancelMessage: function () {
        Ext.MessageBox.alert('Cancel Product');
    }

});