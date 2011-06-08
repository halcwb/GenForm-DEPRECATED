/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:58 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.PackageWindow', {
    extend: 'Ext.Window',
    alias: 'widget.packagewindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.dockedItems = this.createSaveCancelToolBar();
        this.items = this.createPackageForm();

        this.callParent(arguments);
    },

    createPackageForm: function () {
        return { xtype: 'packageform' };
    },

    createSaveCancelToolBar: function () {
        return Ext.create('GenForm.view.component.SaveCancelToolBar', { dock: 'bottom'});
    },

    getPackageForm: function () {
        return this.items.items[0];
    },

    loadWithPackage: function (package) {
        this.getPackageForm().getForm().loadRecord(package);
    }

});