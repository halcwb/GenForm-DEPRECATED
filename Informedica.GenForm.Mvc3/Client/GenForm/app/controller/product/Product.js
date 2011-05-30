/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 21:57
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.controller.product.Product', {
    extend: 'Ext.app.Controller',
    alias: 'widget.productcontroller',

    models: [
        'product.Product',
        'product.GenericName',
        'product.BrandName',
        'product.ShapeName',
        'product.PackageName',
        'product.ProductRoute',
        'product.ProductSubstance'
    ],

    stores: [
        'product.GenericName',
        'GenForm.data.ProductSubstanceTestData'//'GenForm.store.product.ProductSubstanceStore'
    ],

    views: [
        'component.EditableComboBox',
        'product.ProductWindow',
        'product.ProductForm',
        'product.ProductSubstanceForm',
        'product.ProductSubstanceGrid',
    ],

    productWindow: null,
    
    init: function() {
        this.control({
            'productwindow > toolbar button[action=save]': {
                click: this.saveProduct
            },
            'productwindow > toolbar button[action=cancel]': {
                click: this.showCancelMessage
            }
        });

    },

    showProductWindow: function () {
        this.getProductWindow().show();
    },

    getProductWindow: function () {
        if(!this.productWindow) {
            this.productWindow = this.createProductWindow();
            this.loadEmptyProduct(this.productWindow);
        }
        return this.productWindow;
    },

    createProductWindow: function () {
        return Ext.create(this.getProductProductWindowView());    
    },

    getProduct: function (button) {
        return button.up('panel').down('form').getProduct()
    },

    saveProduct: function (button) {
        var me = this,
            product = me.getProduct(button);

        Product.SaveProduct(product.data, {scope: me, callback: me.onProductSaved});
    },

    onProductSaved: function (result) {
        if (result.success) {
            Ext.MessageBox.alert('Product saved: ' + result.data.ProductName);
        } else {
            Ext.MessageBox.alert('Product could not be saved: ' + result.message);
        }
    },

    showCancelMessage: function () {
        Ext.MessageBox.alert('Cancel Product');
    },

    createEmptyProduct: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Product');
    },

    loadEmptyProduct: function (window) {
        window.loadWithProduct(this.createEmptyProduct());
    }

});