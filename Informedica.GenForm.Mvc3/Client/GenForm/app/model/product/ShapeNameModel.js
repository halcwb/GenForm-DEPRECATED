/**
 * Created by .
 * User: hal
 * Date: 7-5-11
 * Time: 6:11
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.ShapeNameModel', {
    extend: 'Ext.data.Model',
    alias: 'widget.shapenamemodel',

    fields: [ {name: 'ShapeName', type: 'string' } ]

/*    proxy: {
        type: 'direct',
        directFn: Product.GetShapeNames
    }*/
});