using System.Collections.Generic;

public class ContextResponse
{
    public ContextElement contextElement { get; set; }
}

public class ContextElement
{
    public List<Attribute> attributes { get; set; }
}

public class Attribute
{
    public string name { get; set; }
    public List<Value> values { get; set; }
}

public class Value
{
    public string _id { get; set; }
    public string recvTime { get; set; }
    public string attrName { get; set; }
    public string attrType { get; set; }
    public double attrValue { get; set; }
}
