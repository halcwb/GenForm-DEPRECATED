/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:57 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ShapeWindow', {
    extend: 'Ext.Window',
    alias: 'widget.shapewindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.dockedItems = this.createSaveCancelToolBar();
        this.items = this.createShapeForm();

        this.callParent(arguments);
    },

    createShapeForm: function () {
        return { xtype: 'shapeform' };
    },

    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.view.component.SaveCancelToolBar', { dock: 'bottom'});
    },

    getShapeForm: function () {
        return this.items.items[0];
    },

    loadWithShape: function (shape) {
        this.getShapeForm().getForm().loadRecord(shape);
    }

});